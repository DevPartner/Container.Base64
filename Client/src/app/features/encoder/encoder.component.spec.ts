import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms'; 
import { EncoderComponent } from './encoder.component';

describe('EncoderComponent', () => {
  let component: EncoderComponent;
  let fixture: ComponentFixture<EncoderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EncoderComponent], // Use declarations for components
      imports: [ReactiveFormsModule]  // Import any necessary modules like ReactiveFormsModule
    })
    .compileComponents();

    fixture = TestBed.createComponent(EncoderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
