import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Make } from 'src/app/_models/make';
import { MakeService } from 'src/app/_services/make.service';

@Component({
  selector: 'app-make-delete',
  templateUrl: './make-delete.component.html',
  styleUrls: ['./make-delete.component.css']
})
export class MakeDeleteComponent implements OnInit {
  make: Make;

  constructor(private makeService: MakeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      // tslint:disable-next-line: no-string-literal
      this.make = data['make'];
    });
  }

  deleteMake() {
    this.makeService.deleteMake(this.make.id).subscribe();
    this.router.navigate(['makes']);
  }

}
