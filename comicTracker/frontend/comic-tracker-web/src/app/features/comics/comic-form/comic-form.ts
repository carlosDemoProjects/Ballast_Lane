import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ComicService } from '../../../core/services/comic.service';
import { Comic } from '../../../core/models/comic.model';

@Component({
  selector: 'app-comic-form',
  standalone: true,
  imports: [ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCheckboxModule],  
  templateUrl: './comic-form.html',
  styleUrl: './comic-form.scss',
})

export class ComicFormComponent {
  private fb = inject(FormBuilder);
  private service = inject(ComicService);
  private ref = inject(MatDialogRef<ComicFormComponent>);
  data: Comic | null = inject(MAT_DIALOG_DATA);

  form = this.fb.group({
    title: [this.data?.title ?? '', Validators.required],
    writer: [this.data?.writer ?? '', Validators.required],
    artist: [this.data?.artist ?? '', Validators.required],
    publisher: [this.data?.publisher ?? ''],    
    readed: [this.data?.readed ?? false]
  });

  submit() {
    if (this.form.invalid) return;
    const payload = this.form.value as any;
    const action = this.data
      ? this.service.update(this.data.id, payload)
      : this.service.create(payload);

    action.subscribe(() => this.ref.close(true));
  }
}