import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeauthorizeComponent } from './deauthorize.component';
import { CookieService } from 'ngx-cookie';
import { DeauthorizeUserService } from './deauthorize-user.service'

describe('DeauthorizeComponent', () => {
  let component: DeauthorizeComponent;
  let fixture: ComponentFixture<DeauthorizeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeauthorizeComponent ],
      providers: [
        CookieService,
        DeauthorizeUserService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeauthorizeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  
});
