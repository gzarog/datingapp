import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editProfileForm', {static: false}) editProfileForm: NgForm;
  user: User;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editProfileForm.dirty) {
      $event.returnValue = true;
    }
  }


  // tslint:disable-next-line: max-line-length
  constructor(private route: ActivatedRoute , private alertify: AlertifyService,
                             private userService: UserService , private authService: AuthService ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
  }


updateUser() {
this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
  this.alertify.success('update success');
  this.editProfileForm.reset(this.user);
 }, error => {
   this.alertify.error(error);
 });
}

}
