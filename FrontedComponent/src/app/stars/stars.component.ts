import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
@Component({
  selector: 'rating-stars',
  templateUrl: './stars.component.html',
  styleUrls: ['./stars.component.scss'],
})
export class StarsComponent implements OnInit {
  @Input() rating: number;
  priorityVector: boolean[] = new Array<boolean>(5);

  constructor() { }

  ngOnInit() {}

  reval(index) {
    this.rating = index > 0 ? index : -index;

    for (let i = 0; i < 5; i++) {
      this.priorityVector[i] = i < this.rating;
    }
  }
}
