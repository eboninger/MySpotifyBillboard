import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleRecordComponent } from './single-record.component';

describe('SingleRecordComponent', () => {
  let component: SingleRecordComponent;
  let fixture: ComponentFixture<SingleRecordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingleRecordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingleRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
