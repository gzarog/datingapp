import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';

import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { BsModalService ,BsModalRef} from 'ngx-bootstrap/modal';



@Component({
  selector: 'app-user-mamagement',
  templateUrl: './user-mamagement.component.html',
  styleUrls: ['./user-mamagement.component.css']
})
export class UserMamagementComponent implements OnInit {
  users : User[];
bsModalRef  : BsModalRef;

  constructor(private adimService : AdminService, private modalService: BsModalService, private alertify : AlertifyService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles (){
    this.adimService.getUsersWithRoles().subscribe((users :User[]) =>{
        this.users =users ;
    }, error => {
      this.alertify.error(error);
    } )
  }

  editRolesModal(){
    const initialState = {
      list: [
        'Open a modal with component',
        'Pass your data',
        'Do something else',
        '...'
      ],
      title: 'Modal with component'
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}

