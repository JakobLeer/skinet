import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from 'src/app/shared/Models/User';
import { Credentials } from 'src/app/shared/Models/Credentials';
import { UserRegistration } from 'src/app/shared/Models/UserRegistration';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = `${environment.apiUrl}/account`;
  private currentUserSource = new BehaviorSubject<User>(null);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,
              private router: Router) { }

  login(credentials: Credentials) {
    return this.http.post(`${this.baseUrl}/login`, credentials)
              .pipe(
                map((user: User) => {
                  localStorage.setItem('token', user.token);
                  this.currentUserSource.next(user);
                })
              );
  }

  /* Does this also log in the user?
     Why not update current user?
  */
  register(userRegistration: UserRegistration) {
    return this.http.post(`${this.baseUrl}/register`, userRegistration)
              .pipe(
                map((user: User) => {
                  this.currentUserSource.next(user);
                })
              );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  // TODO: return value?
  checkEmailExists(email: string) {
    return this.http.get(`${this.baseUrl}/emailexists?email=${email}`);
  }
}
