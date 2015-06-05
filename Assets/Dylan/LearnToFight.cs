//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//
//
//public class LearnToFight : MonoBehaviour
//{
//	public float fadeSpeed = 5f;
//	public GameObject startFightButton;
//	
//	public Canvas tutorialDodgeCanvas;
//	public Canvas tutorialSwipeCanvas;
//	public Canvas tutorialParryCanvas;
//	public Canvas tutorialBlockCanvas;
//	public Canvas tutorialScratchCanvas;
//	public Canvas tutorialStartFightCanvas;
//		
//	public GameObject enemyCharAnime;
//	public GameObject playerCharAnime;
//	
//	readonly int k_isBlocking = Animator.StringToHash("isBlocking");
//	readonly int k_isScratching = Animator.StringToHash("isScratching");
//	
//	readonly int k_isSwipingLeft = Animator.StringToHash("isSwipingLeft");
//	readonly int k_isSwipingRight = Animator.StringToHash("isSwipingRight");
//	readonly int k_isStrikingUp = Animator.StringToHash("isSwipingUp");
//	readonly int k_isStrikingDown = Animator.StringToHash("isSwipingDown");
//	
//	readonly int k_isParryingLeft = Animator.StringToHash("isParryingLeft");
//	readonly int k_isParryingRight = Animator.StringToHash("isParryingRight");
//	readonly int k_isParryingUp = Animator.StringToHash("isParryingUp");
//	readonly int k_isParryingDown = Animator.StringToHash("isParryingDown");
//	
//	readonly int k_isDodgingLeft = Animator.StringToHash("isDodgingLeft");
//	readonly int k_isDodgingRight = Animator.StringToHash("isDodgingRight");
//	
//
//	Animator m_playerAnim;
//	Animator m_enemyAnim;
//	
//
//	enum AnimStateCheckType
//	{
//		idle,
//		Scratch,
//		Strike
//		Block,
//		
//		WaitForBlock,
//		WaitForSwipe,
//		WaitForDodge,
//		WaitForParry,
//		
//		DodgeLeft,
//		DodgeRight,
//		
//		ParryUp,
//		ParryDown,
//		ParryLeft,
//		ParryRight
//	}
//
//	AnimStateCheckType m_checkState = AnimStateCheckType.WaitForBlock;
//
//	// nullable variable type
//	float? m_timer = null;
//
//	
//	void Awake () 
//	{
//		m_playerAnim = playerCharAnime.GetComponent<Animator>();
//		m_enemyAnim = playerCharAnime.GetComponent<Animator>();
//		
//		
//		tutorialStartFightCanvas.enabled = true; 
//		tutorialDodgeCanvas.enabled = false;
//		tutorialSwipeCanvas.enabled = false;
//		tutorialParryCanvas.enabled = false;
//		tutorialBlockCanvas.enabled = false;
//		tutorialScratchCanvas.enabled = false;	
//		//m_player = FindObjectOfType<Player>();
//		
//	}
//	
//	 public void onClick()
//	 {
//		m_enemyAnim.SetBool("clickToStart", true);
//		startFightButton.SetActive(false);
//		
//		//Create Timer for Invoke Fuction
//		m_timer = Time.time + 5f;
//			
//	}
//	
////	Add In Pause System Later	
////	bool m_paused = false;
////	public void Pause()
////	{
////		m_paused = true;
////	}
////	public void Resume()
////	{
////		m_paused = false;
////	}
//	
//	
//	// Update is called once per frame
//	void Update () 
//	{
////		if(m_paused)
////		{
////			if(m_timer.HasValue)
////				m_timer += Time.deltaTime;
////			return;
////		}
//	
//		if(m_timer.HasValue && Time.time > m_timer.Value)
//		{
//			m_timer = null;
//			holdBlockToStart();
//		
//		}
//	
//	
//		switch(m_checkState)
//		{
//			case AnimStateCheckType.Block:
//				if (m_playerAnim.GetBool(k_isBlocking))
//				{
//					m_checkState = AnimStateCheckType.WaitForSwipe;
//					Debug.Log("player is blocking");
//					Debug.Log("BLOCK LEARNED");
//					tutorialBlockCanvas.enabled = false;
//					Time.timeScale = 1;
//				}
//				else 
//				{
//					Debug.Log ("player NOT blocking");
//				}
//				break;
//				
//				
//		case AnimStateCheckType.Scratch:
//			if (m_playerAnim.GetBool(k_isScratching))
//			{
//				m_checkState = AnimStateCheckType.WaitForBlock;
//				Debug.Log("player is blocking");
//				Debug.Log("Player LEARNED to Stop Scratching");
//				tutorialScratchCanvas.enabled = false;
//				Time.timeScale = 1;
//			}
//			else 
//			{
//				Debug.Log ("player NOT Blocking to Start Scratch");
//			}
//			break;
//			
//			
//		case AnimStateCheckType.WaitForBlock:
//			if (m_playerAnim.GetBool(k_isBlocking))
//			{
//				m_checkState = AnimStateCheckType.Blocking;
//				Debug.Log("player is blocking");
//				Debug.Log("Player LEARNED to Block");
//				tutorialScratchCanvas.enabled = false;
//				Time.timeScale = 1;
//			}
//			
//			else 
//			{
//				Debug.Log ("player NOT Blocking to Start Scratch");
//			}
//			break;
//			
//		case AnimStateCheckType.WaitForSwipe:
//			if (m_playerAnim.GetBool((k_isSwipingUp) || m_playerAnim.GetBool(k_isSwipingDown) || m_playerAnim.GetBool(k_isSwipingRight)|| m_playerAnim.GetBool(k_isSwipingLeft) )
//			{
//				m_checkState = AnimStateCheckType.WaitForBlock;
//				Debug.Log("player is Swiping");
//				Debug.Log("Player LEARNED to Swipe");
//				tutorialScratchCanvas.enabled = false;
//				Time.timeScale = 1;
//			}
//			else 
//			{
//				Debug.Log ("player NOT Swiping");
//			}
//			break;
//			
//			case AnimStateCheckType.WaitForParry:
//			if (m_playerAnim.GetBool((k_isSwipingUp) || m_playerAnim.GetBool(k_isSwipingDown) || m_playerAnim.GetBool(k_isSwipingRight)|| m_playerAnim.GetBool(k_isSwipingLeft) )
//			    
//			    {
//				m_checkState = AnimStateCheckType.WaitForBlock;
//				
//				// ADD IN Conditional Statements for Parry Right, Parry Left, Parry Up, Parry Down
//				Debug.Log("player is Swiping");
//				Debug.Log("Player LEARNED to Swipe");
//				tutorialScratchCanvas.enabled = false;
//				Time.timeScale = 1;
//				
//			}
//			else 
//			{
//				Debug.Log ("player NOT Swiping");
//			}
//			break;
//			
//			case AnimStateCheckType.SwipeUp:
//				
//				break;
//				
//		
//		case AnimStateCheckType.WaitForBlock:
//		case AnimStateCheckType.WaitForSwipe:
//			default:
//				break;
//		}
//				
//	}
//	
//	void holdBlockToStart ()
//	{
//	 tutorialStartFightCanvas.enabled = true;
//	 Time.timeScale = 0;
//	 Debug.Log ("GUIFrozen");
//	//	m_checkForBlock = true;
//	
//		m_checkState = AnimStateCheckType.Block;
//
//	}
//	
//	
//	void tillHeadRightToDodge ()
//	{
//		tutorialDodgeCanvas.enabled = true;
//		Time.timeScale = 0;
//		Debug.Log ("GUIFrozen");
//		//	m_checkForBlock = true;
//				
//		m_checkState = AnimStateCheckType.tillHeadLeftToDodge ()
//	}
//	
//	void tillHeadLeftToDodge ()
//	{
//	 tutorialDodgeCanvas.enabled = true;
//	 Time.timeScale = 0;
//	 Debug.Log ("GUIFrozen");
//	//	m_checkForBlock = true;
//	
//		m_checkState = AnimStateCheckType.void holdBlockToStart ()
//	}
//	
//}
