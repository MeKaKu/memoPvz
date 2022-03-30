using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelManager : MonoBehaviour
{
    public PlantAssetId id;
    public Chooseable shovel;
    public ChosePlantPlane chosePlantPlane;

    private void Start() {
        shovel.onPointerEnter += (self)=>{
            if(self.isChosen) return;
            self.DoMouseHover();
        };
        shovel.onPointerDown += (self)=>{
            self.isChosen = !self.isChosen;
            if(self.isChosen){
                self.UndoMouseHover();
                self.HideInfo();
                chosePlantPlane.ShowPlant(id);
                shovel.Hide();
            }
        };
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1)){
            chosePlantPlane.Hide();
            shovel.Show();
        }
    }
}
