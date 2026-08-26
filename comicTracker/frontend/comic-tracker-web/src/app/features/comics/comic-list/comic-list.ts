import { Component, inject, OnInit, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { ComicService } from '../../../core/services/comic.service';
import { Comic } from '../../../core/models/comic.model';
import { ComicFormComponent } from '../comic-form/comic-form';

@Component({
  selector: 'app-comic-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatIconModule, MatDialogModule, MatCardModule],
  templateUrl: './comic-list.html',
  styleUrl: './comic-list.scss',
})

export class ComicListComponent implements OnInit {
  private service = inject(ComicService);
  private dialog = inject(MatDialog);
  comics = signal<Comic[]>([]);
  columns = ['title', 'writer', 'artist', 'publisher', 'readed', 'actions'];

  ngOnInit() { this.load(); }

  load() {
    this.service.getAll().subscribe(data => this.comics.set(data));
  }

  openForm(comic?: Comic) {
    const ref = this.dialog.open(ComicFormComponent, {
      width: '420px',
      data: comic ?? null
    });
    ref.afterClosed().subscribe(result => { if (result) this.load(); });
  }

  delete(id: string) {
    if (confirm('Delete this comic?')) {
      this.service.delete(id).subscribe(() => this.load());
    }
  }
}
