import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';
import { AuthGuard } from './auth.guard'; // 🔥 Brug stor A!

describe('AuthGuard', () => {
  let guard: AuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthGuard); // 🔥 Brug TestBed til at injicere AuthGuard
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
