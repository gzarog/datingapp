import { Component, OnInit } from '@angular/core';
import { EmailTemplate } from 'src/app/_models/emailTemplate';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup,Validators } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';


@Component({
  selector: 'app-emailTemplate-modal',
  templateUrl: './emailTemplate-modal.component.html',
  styleUrls: ['./emailTemplate-modal.component.css']
})
export class EmailTemplateModalComponent implements OnInit {
  emailTemplate : EmailTemplate;
  emailTemplateForm : FormGroup;
  constructor(public bsModalRef: BsModalRef,private formBuilder: FormBuilder) { }

  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      ['bold']
      ],
    customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ]
  };


  ngOnInit() {
    this.createEmailTemplateForm();
  }

  createEmailTemplateForm() {
    this.emailTemplateForm = this.formBuilder.group({
      id: [this.emailTemplate.id, Validators.required],
      name: [this.emailTemplate.name, Validators.required],
      content: [this.emailTemplate.content],
    });
  }

  saveEmailTemplate(){

  }


}
