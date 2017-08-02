import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackListComponent } from './track-list.component';
import { RoundTwoDecPipe } from './../../round-two-dec.pipe'
import { CreatePlaylistComponent } from './create-playlist/create-playlist.component'
import { RouterTestingModule } from '@angular/router/testing'

describe('TrackListComponent', () => {
  let component: TrackListComponent;
  let fixture: ComponentFixture<TrackListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ 
        TrackListComponent,
        RoundTwoDecPipe,
        CreatePlaylistComponent
      ],
      imports: [
        RouterTestingModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrackListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


});
