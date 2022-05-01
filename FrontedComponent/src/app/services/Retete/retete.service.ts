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

        //console.log(retetaCurenta.nume_reteta)
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
      //console.log(reteta , "reteta")
      return this.groupBy(reteta)[0] //nu merge incarcare modal
    }

    async allRetete(){
      var reteta = (await this.http.get(environment.baseUrl+"Retete/all").toPromise() as Array<any>);
     // console.log(reteta)
      return reteta
    }
    async toggleLike(model_title){
      console.log (model_title, localStorage.getItem('token'))
      var reteta = (await this.http.post(environment.baseUrl+"Retete/like",{Name:model_title, Token:localStorage.getItem('token')}).toPromise() as Array<any>);
     // console.log(reteta, 'toggle');
    }

    async getRetete(initialRequest:boolean):Promise<any>{
      var result = (await this.http.post(environment.baseUrl+"Retete",{initialRequest}).toPromise() as Array<any>);
     // console.log(retete)
      //var result = this.groupBy(retete)
      var likedRetete = (await this.http.post(environment.baseUrl+"Aprecieri/GetById",{token:localStorage.getItem('token')}).toPromise() as Array<any>);
     // console.log(likedRetete, result)
      likedRetete.forEach(lR =>{
     //   console.log(lR);
        try{
        (result.find((x:any) =>{ console.log(x.id, lR.idReteta, x.id == lR.idReteta);  return x.id == lR.idReteta;}) as any).liked = true;
        }catch(e){console.log(e)}
      })
      return result

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

    getLikedRetete():Promise<any>{


      return this.http.post(environment.baseUrl+"Aprecieri/GetById",{token:localStorage.getItem('token')}).toPromise();
    }


     async getReteta(idReteta:string):Promise<any>{
      var rezultat = (await this.http.get(environment.baseUrl+"Retete/liked/"+idReteta).toPromise()) as Array<any>;
      return this.groupBy(rezultat)
    }



}

