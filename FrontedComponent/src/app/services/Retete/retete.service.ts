import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReteteService {




  constructor(private http:HttpClient) { }

    private groupBy(array:Array<any>){
      var retete = {}
      array.forEach(item=>{
        var idReteta = item['id']
        if(!retete[idReteta]){
          retete[idReteta] = {}
        }
        var retetaCurenta = retete[idReteta];

        if(!retetaCurenta.nume_reteta){
          retetaCurenta.nume_reteta = item['nume_reteta']
          retetaCurenta.nume_Tip_Retete = item['nume_Tip_Retete']
          retetaCurenta.id = item['id']
          retetaCurenta.retetaIngredient = []
          retetaCurenta.tipReteta= {nume_Tip_Retete:item['nume_Tip_Retete']}
          retetaCurenta.poza_reteta = item['poza_reteta']
          retetaCurenta.instructiuni=item['instructiuni_reteta']
          retetaCurenta.nume_pahar=item['nume_Pahar']
          retetaCurenta.rating=item['rating_retea']
        }

        console.log(retetaCurenta.nume_reteta)
        retetaCurenta.retetaIngredient.push({
          nume_ingredient:item['nume_ingredient'],
          idIngredient:item['idIngredient'],
          unitate:item['nume_unitate'],
          cantitate: item['cantitate_Ingredient']

        });
      })

      return Object.values(retete);
    }

    async getRetetaRandom(){
      var reteta = (await this.http.get(environment.baseUrl+"Retete/random").toPromise() as Array<any>);
      console.log(reteta)
      return this.groupBy(reteta)[0]
    }

    async toggleLike(model_title){
      console.log (model_title, localStorage.getItem('token'))
      var reteta = (await this.http.post(environment.baseUrl+"Retete/like",{Name:model_title, Token:localStorage.getItem('token')}).toPromise() as Array<any>);
      console.log(reteta, 'toggle');
      return this.groupBy(reteta)[0]
    }

    async getRetete(page:Number, recordsPerPage:Number):Promise<any>{
      var retete = (await this.http.post(environment.baseUrl+"Retete",{page,recordsPerPage}).toPromise() as Array<any>);
      console.log(retete)
      var result = this.groupBy(retete)
      var likedRetete = (await this.http.post(environment.baseUrl+"Aprecieri/GetById",{token:localStorage.getItem('token')}).toPromise() as Array<any>);
      console.log(likedRetete, result)
      likedRetete.forEach(lR =>{
        console.log(lR);
        try{
        (result.find((x:any) =>{ console.log(x.id, lR.idReteta, x.id == lR.idReteta);  return x.id == lR.idReteta;}) as any).liked = true;
        }catch(e){console.log(e)}
      })
      return result

      // for(let index in retete){
      //    var reteta = retete[index];
      //    reteta.tipReteta = await this.getTipReteta(reteta.idTipReteta);
      //    reteta.retetaIngredient=await this.getIngrediente(reteta.id);
      //    for (let index2 in reteta.retetaIngredient )
      //    {
      //       var ingredient_aux= reteta.retetaIngredient[index2];
      //       ingredient_aux.ingredient=await this.getNumeIngredient(ingredient_aux.idIngredient);
      //       //console.log( ingredient_aux.ingredient.nume_ingredient)



      //       //console.log( reteta.nume_reteta,  reteta.retetaIngredient)
      //    }
      //    }
      //    reteta.retetaIngredient
      //   console.log(retete);
      // return retete;
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





  /*
    id: "0b7f875b-9e46-4773-8b6d-01bed26b7961"
idIngredient: "25099c74-157a-4974-b522-2fb5f31f09cf"
idReteta: "0b7f875b-9e46-4773-8b6d-01bed26b7961"
idTipReteta: "6d16ee3f-76a8-4015-86f0-f4565f2215ee"
nume_Tip_Retete: "Alcoolic"
nume_ingredient: "Rom"
nume_reteta: "Barracuda"
retetaIngredient: (5) [{…}, {…}, {…}, {…}, {…}]
tipReteta: {nume_Tip_Retete: 'Alcoolic', retete: null, id: '6d16ee3f-76a8-4015-86f0-f4565f2215ee', dateCreated: '2022-03-04T13:17:23.5057226', dateModified: null}
*/


/*
id: "0b7f875b-9e46-4773-8b6d-01bed26b7961"
idIngredient: "25099c74-157a-4974-b522-2fb5f31f09cf"
idReteta: "0b7f875b-9e46-4773-8b6d-01bed26b7961"
idTipReteta: "6d16ee3f-76a8-4015-86f0-f4565f2215ee"
nume_Tip_Retete: "Alcoolic"
nume_ingredient: "Rom"
nume_reteta: "Barracuda"*/
