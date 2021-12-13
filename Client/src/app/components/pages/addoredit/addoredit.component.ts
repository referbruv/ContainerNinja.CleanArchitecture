import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateOrUpdateItemDTO } from 'src/app/models/CreateOrUpdateItemDTO';
import { ItemDTO } from 'src/app/models/ItemDTO';
import { ItemsService } from 'src/app/providers/items.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.scss']
})
export class AddoreditComponent implements OnInit {

  id: any = 0;
  item: CreateOrUpdateItemDTO = {
    name: '',
    description: '',
    colorCode: '#FFFFFF',
    categories: ''
  };
  form: FormGroup;
  isEdit: boolean = false;
  errors: string[] = [];
  isError: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private itemsService: ItemsService, private formBuilder: FormBuilder, private router: Router) {
    this.form = this.formBuilder.group({
      name: new FormControl(this.item.name, []),
      description: new FormControl(this.item.description, []),
      categories: new FormControl(this.item.categories, []),
      colorCode: new FormControl(this.item.colorCode, [])
    })
  }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((map) => {
      if (map.has('id')) {
        this.id = map.get('id');
        this.itemsService.getById(this.id).subscribe(x => {
          this.item = {
            name: x.name,
            description: x.description,
            categories: x.categories,
            colorCode: x.colorCode
          }
          this.form.setValue(this.item);
          this.isEdit = true;
        })
      }
    });
  }

  public get Item() {
    if (this.item === null) return null;
    else return this.item;
  }

  save() {
    console.log(this.form.value);
    if (this.isEdit) {
      this.itemsService.update(this.id, <CreateOrUpdateItemDTO>this.form.value).subscribe({
        next: res => {
          this.back();
        },
        error: (err) => {
          if (err.ok === false) {
            this.isError = true;
            let e = err.error;
            this.errors = <string[]>e.errors;
          }
        }
      });
    } else {
      this.itemsService.add(<CreateOrUpdateItemDTO>this.form.value).subscribe({
        next: res => {
          this.back();
        },
        error: (err) => {
          if (err.ok === false) {
            this.isError = true;
            let e = err.error;
            this.errors = <string[]>e.errors;
          }
        }
      });
    }
  }

  delete() {
    let response = confirm('Are you sure you want to delete?');
    if (response === true) {
      this.itemsService.delete(this.id).subscribe({
        next: res => {
          this.back();
        },
        error: (err) => {
          if (err.ok === false) {
            this.isError = true;
            let e = err.error;
            this.errors = <string[]>e.errors;
          }
        }
      });
    }
  }

  back() {
    this.router.navigate(['items']);
  }

}
