import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/_models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'admin/usersWithRoles').toPromise()
    .then(res => <User[]> res)
    .then(data => { return data; });
  }
  updateUserRoles(user: User, roles: {}){
    return this.http.post(this.baseUrl + 'admin/editRoles/' + user.userName, roles);

    }

  

}

