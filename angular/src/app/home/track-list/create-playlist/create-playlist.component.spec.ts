import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlaylistComponent } from './create-playlist.component';
import { RouterTestingModule } from '@angular/router/testing'
import { CookieService } from 'ngx-cookie-service'
import { KeyService } from './../../../key.service'

describe('CreatePlaylistComponent', () => {
  let component: CreatePlaylistComponent;
  let fixture: ComponentFixture<CreatePlaylistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatePlaylistComponent ],
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
    fixture = TestBed.createComponent(CreatePlaylistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  
});
