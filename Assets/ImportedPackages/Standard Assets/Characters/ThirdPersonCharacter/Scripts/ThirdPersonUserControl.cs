using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
		public static ThirdPersonUserControl Instance;

		public GameObject trueEffect, falseEffect;
		private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        public Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public static bool disabled = false;

		public Joystick joystick;

		public bool isRunning = false;

		float h;
		float v;
		bool crouch;

		private void Awake()
		{
			Instance = this;
		}
		private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (disabled) return;
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }
		public void JumpNow()
		{
			m_Jump = true;
		}
		public void Run(bool isRunning)
		{
			this.isRunning = isRunning;
			Debug.Log("Is Running: " + isRunning);
		}

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (disabled) {
                m_Character.Move (Vector3.zero, false, false);
                return ;
            }
			// read inputs
#if MOBILE_INPUT
			h = joystick.Horizontal;
			v = joystick.Vertical;
			Debug.Log(h + ", " + v);
#else
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
#endif

			// calculate move direction to pass to character
			if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (!Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, false, m_Jump);
            m_Jump = false;
        }

		private void OnTriggerEnter(Collider other)
		{
			switch (other.tag)
			{
				case "TrueMCQ":
					//QuestionsManager.mcqQuestions.Remove(MCQ_Manager.question);
					Instantiate(trueEffect, other.transform.position, Quaternion.identity);
					QuestionsManager.Instance.ClearAllQuestionGames();
					StartCoroutine(QuestionsManager.Instance.ReportQuestionsStatus(MCQ_Manager.question.id, 1));
					PlayerStats.CorrectAnswers++;
					break;
				case "False":
					Instantiate(falseEffect, other.transform.position, Quaternion.identity);
					QuestionsManager.Instance.ClearAllQuestionGames();
					StartCoroutine(QuestionsManager.Instance.ReportQuestionsStatus(MCQ_Manager.question.id, 0));
					break;
				case "Leave":
					Leave();
					break;
				default:
					break;
			}
		}

		void Leave()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		}
	}
}
