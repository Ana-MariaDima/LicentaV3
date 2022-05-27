import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalServiceService } from '../modal-service.service';
import { ReteteService } from '../services/Retete/retete.service';
import { Tab1PageModule } from '../tab1/tab1.module';
import { Tab1Page } from '../tab1/tab1.page';

@Component({
  selector: 'modal-popup-ing-all',
  templateUrl: './modal-popup-all.page.html',
  styleUrls: ['./modal-popup-all.page.scss'],
})
export class ModalPopupIngAll implements OnInit {
  // @Input() model_title: string;
  // @Input() subcategorie: any;
   //@Input() ingrediente: any;

  searchTerm: string;
  itemsInCart: any[]=[];
  selected:any = {};
  ingredienteAll:Array<any> = []
  constructor(
    private modalController: ModalController,
    private modalService:ModalServiceService,
    private ReteteService: ReteteService

  ) {

   }


 async ngOnInit() {
  if(localStorage.getItem("cart") != undefined)
  {
    this.refreshCart()
  }
  var temp= (await this.ReteteService.getIngredienteAll());
  // for (let i in temp){
  //   var ing:any =temp[i];
  //   ing.subCategorieIngredient= await this.ReteteService.getSubcategIngredient(ing.idSubCategorieIngredient);
  //   console.log("Poza Subcateg in cart", ing.subCategorieIngredient.pozaSubcategorieIngredient);
  // }
  this.ingredienteAll=temp;





    // this.ingredienteAll = (await this.ReteteService.getIngredienteAll());
    // this.ingredienteAll.forEach(async ing => {
    //   ing.subCategorieIngredient=  await this.ReteteService.getSubcategIngredient(ing.idSubCategorieIngredient);



    // });
    console.log(this.ingredienteAll);
    //sa pun poza? da nu?

}

addtoCart(item)
  {
    this.selected[item.id] = true;
    item.quantityInCart += 1;
    this.itemsInCart.push(item);
    //console.log( item.quantityInCart);
    var cart = localStorage.getItem('cart')? JSON.parse(localStorage.getItem('cart')):{}
    cart[item.id] = item
    localStorage.setItem('cart', JSON.stringify(cart));

  }

  removeFromCart(item){
    this.selected[item.id] = false;
    var newCart = {}
    this.itemsInCart.forEach(x=>{
      if(x.id != item.id){
        newCart[x.id] = x
      }
    });
   // this.itemsInCart=newCart;
   item.quantityInCart -= 1;
    this.itemsInCart = Object.values(newCart);

    //console.log("deleted ",item, this.itemsInCart)

    localStorage.setItem('cart', JSON.stringify(newCart))
  }

  async dismiss() {

     var result= this.modalController.dismiss({customProp:"123"});

     return result
  }
  refreshCart(){
    this.itemsInCart = Object.values(JSON.parse(localStorage.getItem('cart')))
    this.ingredienteAll.forEach(x=>{ this.selected[x.id] = this.itemsInCart.map(z=>z.id).includes(x.id);});

  }
  async openCart(){
    this.modalController.dismiss();
    await this.modalService.openCartModal()
    //this.refreshCart();

 }
}
