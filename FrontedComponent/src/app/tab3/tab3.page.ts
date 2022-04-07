import { Component } from '@angular/core';
import { Tab1ToateRetetelePage } from '../tab1-toate-retetele/tab1-toate-retetele.page';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {

  constructor(private Tab1ToateRetetelePage:Tab1ToateRetetelePage ) {}
  PreopenCardModal(param){
    this.Tab1ToateRetetelePage.openCardModal(param);
  }


}
