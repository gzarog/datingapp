import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { EmailTemplate } from 'src/app/_models/emailTemplate';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  getEmailTemplates(){
    return this.http.get(this.baseUrl + 'settings/emailtemplates')
  }

  editEmailTemplate(id :number ,emailTemplate: EmailTemplate){
    return this.http.post(this.baseUrl + 'settings/editEmailTemplate/' + id, emailTemplate);

    }



}
