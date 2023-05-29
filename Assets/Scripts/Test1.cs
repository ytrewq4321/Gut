using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class Test1 : MonoBehaviour
{
    private PlayMakerFSM fsm;

    public void Start()
    {
        fsm = GetComponent<PlayMakerFSM>(); 

        FsmState button = fsm.FsmStates[0];
        button.Name = "ButtonClick";
    
        FsmState wait = new FsmState(fsm.Fsm);
        wait.Name = "Wait";

        FsmState debugLog = new FsmState(fsm.Fsm);
        debugLog.Name = "DebugLog";

        FsmTransition buttonTransition = new FsmTransition();
        FsmTransition waitTransition = new FsmTransition();
        FsmTransition debugLogTransition = new FsmTransition();

        buttonTransition.ToFsmState = wait;
        buttonTransition.FsmEvent = FsmEvent.GetFsmEvent("UI CLICK");
        button.Transitions = new FsmTransition[] { buttonTransition };

        waitTransition.ToFsmState = debugLog;
        waitTransition.FsmEvent = FsmEvent.GetFsmEvent("FINISHED");
        Wait waitAction = new Wait();
        waitAction.time = 3f;
        wait.Actions = new FsmStateAction[] { waitAction };
        wait.Transitions = new FsmTransition[] { waitTransition };

        debugLogTransition.ToFsmState = button;
        debugLogTransition.FsmEvent = FsmEvent.GetFsmEvent("FINISHED");
        DebugLog debugLogAction = new DebugLog();
        debugLogAction.text = "Hello!";
        debugLogAction.logLevel = LogLevel.Info;
        debugLogAction.sendToUnityLog = true;
        debugLog.Actions = new FsmStateAction[] { debugLogAction };
        debugLog.Transitions = new FsmTransition[] { debugLogTransition };

        fsm.Fsm.States = new FsmState[] { button,wait,debugLog };
        fsm.Fsm.StartState = "ButtonClick";
    }
}
