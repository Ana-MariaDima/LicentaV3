import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { Tab1ToateRetetelePage } from './tab1-toate-retetele.page';

describe('Tab1ToateRetetelePage', () => {
  let component: Tab1ToateRetetelePage;
  let fixture: ComponentFixture<Tab1ToateRetetelePage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ Tab1ToateRetetelePage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(Tab1ToateRetetelePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
