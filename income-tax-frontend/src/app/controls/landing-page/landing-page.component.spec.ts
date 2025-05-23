import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { createSpyFromClass, Spy } from 'jasmine-auto-spies';
import { LandingPageComponent } from './landing-page.component';

// Dummy component
@Component({
  selector: 'app-main-content-wrapper',
  template: '<ng-content></ng-content>'
})
class DummyMainContentWrapperComponent {}

describe('Component: LandingPageComponent', () => {
  let component: LandingPageComponent;
  let fixture: ComponentFixture<LandingPageComponent>;
  let routerSpy: Spy<Router>;
  
  beforeEach(async () => {
    routerSpy = createSpyFromClass(Router);
    
    await TestBed.configureTestingModule({
      declarations: [LandingPageComponent, DummyMainContentWrapperComponent],
      providers: [{ provide: Router, useValue: routerSpy }]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should call router when goToCalculator is called', () => {
    component.goToCalculator();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/calculate']);
  });
});
