import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-records',
  templateUrl: './records.component.html',
  styleUrls: ['./records.component.css']
})
export class RecordsComponent implements OnInit {
  longNumberOne: string = "Photosynthesis - Saba (3 Days)"
  longTime: string = "Suede - NxWorries (173 Days)"
  longNumberOneCons: string = "Suede - NxWorries (156 Days)"
  longTimeCons: string = "For Free? - Interlude - Kendrick Lamar (198 Days)"
  
  constructor() { }

  ngOnInit() {
  }

}
