import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FinishAuthComponent } from './finish-auth.component';
import { RouterTestingModule } from '@angular/router/testing';
import { CookieService } from 'ngx-cookie-service';
import { KeyService } from '../../key.service';

describe('FinishAuthComponent', () => {
  let component: FinishAuthComponent;
  let fixture: ComponentFixture<FinishAuthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        FinishAuthComponent,
      ],
      imports: [
        RouterTestingModule
      ],
      providers: [
        CookieService,
        KeyService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinishAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
