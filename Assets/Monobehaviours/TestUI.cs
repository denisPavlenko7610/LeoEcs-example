using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;

public class TestUI : EcsUguiCallbackSystem {
    [Preserve] // Этот атрибут необходим для сохранения этого метода для il2cpp.
    [EcsUguiClickEvent]
    void OnAnyClick (in EcsUguiClickEvent evt) {
        Debug.Log ("Im clicked!", evt.Sender);
    }
    
    // Этот метод будет вызван при нажатии на виджет с действием, имеющим имя "exit-button". 
    [Preserve]
    [EcsUguiClickEvent("exit-button")]
    void OnExitButtonClicked (in EcsUguiClickEvent evt) {
        Debug.Log ("exit-button clicked!", evt.Sender);
    }
}