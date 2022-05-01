import { Component } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { ModalPopupPage } from '../modal-popup-ing/modal-popup.page';
import { ModalServiceService } from '../modal-service.service';
import { CategoryService } from '../services/Ingrediente/category.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})


export class Tab1Page {
  categorii:Array<any> = []
  subcategorii: Array<any> = []
  ingrediente:Array<any> = []
  imagesUrl = environment.imagesUrl
  itemsInCart: number;
  searchTerm: string;


  constructor(private categoriiService: CategoryService, private modalService:ModalServiceService) {

  }

  async ngOnInit(){
    this.categorii = await this.categoriiService.getCategoriiIngrediente()
    /*for( let index in this.categorii){
      var categorie=this.categorii[index]
      //this.subcategorii=await this.categoriiService.getSubCategoriiIngrediente(categorie["id"])
     categorie.subCategoriiIngrediente= await this.categoriiService.getSubCategoriiIngrediente(categorie.id)
     // console.log(categorie.subCategoriiIngrediente, "categorie " )

      for (let index2 in categorie.subCategoriiIngrediente){
        var subcategorie =categorie.subCategoriiIngrediente[index2]
        categorie.subCategoriiIngrediente.ingrediente =await this.categoriiService.getIngrediente (subcategorie.id)
        //console.log( categorie.subCategoriiIngrediente.ingrediente , "ingredient")
        this.ingrediente+= categorie.subCategoriiIngrediente.ingrediente
        console.log(this.ingrediente, "toate ingredientele ")
      }

    }

   // this.ingrediente=  await this.Ingredient.allRetete();
    */


  }

  ionViewWillEnter(){

    var items = JSON.parse(localStorage.getItem('cart'))
    if(items){
    this.itemsInCart = Object.values(items).length;
    console.log(this.itemsInCart)
    }
  }

  async openCart(){
    await this.modalService.openCartModal()
    this.ionViewWillEnter();
  }
  async openCardModal(subcategorie){
    await this.modalService.openIngredientModal(subcategorie);
    this.ionViewWillEnter();
  }
}
