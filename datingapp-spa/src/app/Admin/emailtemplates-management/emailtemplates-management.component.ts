import { Component, OnInit } from '@angular/core';
import { EmailTemplate } from 'src/app/_models/emailTemplate';
import { SettingsService } from 'src/app/_services/settings.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EmailTemplateModalComponent } from '../emailTemplate-modal/emailTemplate-modal.component';

@Component({
  selector: 'app-emailtemplates-management',
  templateUrl: './emailtemplates-management.component.html',
  styleUrls: ['./emailtemplates-management.component.css']
})
export class EmailtemplatesManagementComponent implements OnInit {
  emailTemplates :EmailTemplate[];
  bsModalRef  : BsModalRef;
  constructor(private settingsService: SettingsService,private modalService: BsModalService, private alertify : AlertifyService) {    }

  ngOnInit() {
      this.getEmailTemplates();
  }

  getEmailTemplates (){
    this.settingsService.getEmailTemplates().subscribe((emailTemplates :EmailTemplate[]) =>{
        this.emailTemplates = emailTemplates ;
    }, error => {
      this.alertify.error(error);
    } )
  }

  editEmailTemplate(emailTemplate :EmailTemplate){
    const initialState = {
      emailTemplate
    };
    this.bsModalRef = this.modalService.show(EmailTemplateModalComponent, {initialState});


  }

}
