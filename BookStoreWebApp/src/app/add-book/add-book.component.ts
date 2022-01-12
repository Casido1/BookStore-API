import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.scss']
})
export class AddBookComponent implements OnInit {

  public bookForm: FormGroup;

  constructor(private formBuilder: formBuilder) { }

  ngOnInit(): void {
    this.init();
  }

  private init(): void{
    this.bookForm = this.formBuilder.group({
      title : [],
      Description : []
    });
  }

}
