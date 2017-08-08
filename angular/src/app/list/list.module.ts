import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BusyModule } from 'angular2-busy'

import { CreatePlaylistComponent } from './track-list/create-playlist/create-playlist.component';
import { RecordsComponent } from './records/records.component';
import { SingleRecordComponent } from './records/single-record/single-record.component';
import { ListComponent } from './list.component';
import { TrackListComponent } from './track-list/track-list.component';

import { TopTracksService } from './track-list/top-tracks.service';
import { CreatePlaylistService } from './track-list/create-playlist/create-playlist.service'
import { SerializeTracksService } from './serialize-tracks.service';

import { RoundTwoDecPipe } from './round-two-dec.pipe';

@NgModule({
  declarations: [
    CreatePlaylistComponent,
    RecordsComponent,
    SingleRecordComponent,
    ListComponent,
    TrackListComponent,
    RoundTwoDecPipe
  ],
  imports: [
    CommonModule,
    BusyModule
  ],
  providers: [
    TopTracksService,
    SerializeTracksService,
    CreatePlaylistService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class ListModule { }
