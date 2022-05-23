import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor() {
    // eventRehistry {numeEveniment: [callbackuri_diversi_abonati]}

    (window as any).EventSystem = new EventSystem();
  }
}

class EventSystem {
  eventRegistry:any = {};

    listen(eventName, callback){
      this.eventRegistry[eventName] = this.eventRegistry[eventName]?this.eventRegistry[eventName] :[]

      this.eventRegistry[eventName].push(callback);
    }

    isEventValid(eventName){
        var eventCategories = eventName.split(">>")
        //retetaReviewed
        //retetaReviewed.nume=sdads

        for(let i=0; i< eventCategories.length; i++){
           var tmp = [];//eventCategories[i];
            for(let x = 0; x<=i; x++){
             tmp.push(eventCategories[x]);
            }
          var possibleListeners = this.eventRegistry[tmp.join(".")]

        }

        //z.x.t.h=sdsa
        //z
        //z.x
        //z.x.t

    }

    trigger(eventName, eventParams){
      //
      if(!this.eventRegistry)
        return;

        console.log(eventName, "triggered with ",eventParams);
        var eventCategories = eventName.split(">>")

        for(let i=0; i< eventCategories.length; i++){
          var tmp = [];//eventCategories[i];
           for(let x = 0; x<=i; x++){
            tmp.push(eventCategories[x]);
           }
         var possibleListeners = this.eventRegistry[tmp.join(">>")]
         console.log("looking for listeners for ev ",tmp.join(">>"), possibleListeners);

         if(!possibleListeners || !possibleListeners.length) continue;

         possibleListeners.forEach(callback=>{
            try{
              callback(eventParams)
            }catch(e){console.warn(e);}
          })
       }


      //this.eventRegistry[eventName]
    }

    unsubscribe(eventName){
      delete this.eventRegistry[eventName]
    }}

