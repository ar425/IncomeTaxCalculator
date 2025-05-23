import { TestBed } from '@angular/core/testing';
import { MainContentWrapperComponent } from './main-content-wrapper.component';

describe('Component: MainContentWrapperComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MainContentWrapperComponent],
    }).compileComponents();
  });

  it('should be truthy', () => {
    const fixture = TestBed.createComponent(MainContentWrapperComponent);
    const component = fixture.componentInstance;
    expect(component).toBeTruthy();
  });
});
