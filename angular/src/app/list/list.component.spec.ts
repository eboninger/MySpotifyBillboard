import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { TrackListComponent } from './track-list/track-list.component'
import { RoundTwoDecPipe } from '../round-two-dec.pipe'
import { CreatePlaylistComponent } from './track-list/create-playlist/create-playlist.component'
import { RouterTestingModule } from '@angular/router/testing'
import { SerializeTracksService } from './serialize-tracks.service'
import { CookieService } from 'ngx-cookie-service'
import { KeyService } from './../key.service'


describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        HomeComponent,
        TrackListComponent,
        RoundTwoDecPipe,
        CreatePlaylistComponent,
       ],
      imports: [
        RouterTestingModule
      ],
      providers: [
        SerializeTracksService,
        CookieService,
        KeyService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


});
