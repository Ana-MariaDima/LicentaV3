import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReteteService {

  constructor(private http:HttpClient) { }




    async getRetete():Promise<any>{
      var retete = await this.http.get(environment.baseUrl+"Retete").toPromise();
      for(let index in retete){
         var reteta = retete[index];
         reteta.tipReteta = await this.getTipReteta(reteta.idTipReteta);
         reteta.retetaIngredient=await this.getIngrediente(reteta.id);
         for (let index2 in reteta.retetaIngredient )
         {
            var ingredient_aux= reteta.retetaIngredient[index2];
            ingredient_aux.ingredient=await this.getNumeIngredient(ingredient_aux.idIngredient);
            //console.log( ingredient_aux.ingredient.nume_ingredient)



            //console.log( reteta.nume_reteta,  reteta.retetaIngredient)
         }
         }
         reteta.retetaIngredient

      return retete;
    }

    getTipReteta(idTipReteta:string):Promise<any>{
      return this.http.get(environment.baseUrl+"TipuriRetete/"+idTipReteta).toPromise();
    }
    getIngrediente(idReteta:string):Promise<any>{
      return this.http.get(environment.baseUrl+"ReteteIngrediente/GetByReteta/"+idReteta).toPromise();
    }
    getNumeIngredient(idIngredient:string):Promise<any>{
      return this.http.get(environment.baseUrl+"Ingrediente/"+idIngredient).toPromise();
    }



}
