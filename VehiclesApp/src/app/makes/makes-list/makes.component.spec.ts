/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MakesComponent } from './makes.component';

describe('MakesComponent', () => {
  let component: MakesComponent;
  let fixture: ComponentFixture<MakesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MakesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MakesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
