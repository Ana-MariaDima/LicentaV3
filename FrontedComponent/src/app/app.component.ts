import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor() {
    // eventRehistry {numeEveniment: [callbackuri_diversi_abonati]}

    (window as any).EventSystem = {eventRegisty:{},

    listen:function(eventName, callback){
      this.eventRegistry[eventName] = this.eventRegisty[eventName]?this.eventRegisty[eventName] :[]

      this.eventRegistry[eventName].push(callback);
    }, trigger:function(eventName, eventParams){

      this.eventRegistry[eventName].forEach(callback=>{
        try{
          callback(eventParams)
        }catch(e){console.warn(e);}
      })
    }, unsubscribe: function(eventName){
      delete this.eventRegistry[eventName]
    }}
  }
}

