import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { ReteteService } from '../services/Retete/retete.service';

@Component({
  selector: 'app-modal-popup-cart',
  templateUrl: './modal-popup-cart.page.html',
  styleUrls: ['./modal-popup-cart.page.scss'],
})
export class ModalPopupCartPage implements OnInit {
  items:Array<any>
  itemsInCart: any[]=[];
  buttonDisabled:boolean =false;
  imagesUrl = environment.imagesUrl;

  perfectMatch = false;

  constructor(private modalController:ModalController, private reteteService: ReteteService) {

  }

  async ngOnInit(): Promise<void> {
    if(localStorage.getItem("cart") != undefined)
    {
    var temp= Object.values(JSON.parse(localStorage.getItem('cart')));
    for (let i in temp){
      var ing:any =temp[i];
      ing.subCategorieIngredient= await this.reteteService.getSubcategIngredient(ing.idSubCategorieIngredient);
      console.log("Poza Subcateg in cart", ing.subCategorieIngredient.pozaSubcategorieIngredient);
    }
    this.itemsInCart=temp;

    console.log("Items in cart", this.itemsInCart);
    }
  }


  removeFromCart(ingredient){
    var newCart = {}
    this.itemsInCart.forEach(x=>{
      if(x.id != ingredient.id){
        newCart[x.id] = x
      }
    });
   // console.log("before delete ", this.itemsInCart.length);
    this.itemsInCart = Object.values(newCart);

   // console.log("deleted ", this.itemsInCart.length)

    localStorage.setItem('cart', JSON.stringify(newCart))
  }

  async dismiss() {
    const close: string = "Modal Removed";
    return this.modalController.dismiss({customProp:"123"});
  }

  genertaePerfectMatch(){
    setTimeout(()=>{
      this.dismiss();
    }, 500)


  }

}
