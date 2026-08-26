import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Comic, SaveComic } from '../models/comic.model';

@Injectable({ providedIn: 'root' })
export class ComicService {
  private readonly API = 'http://localhost:5202/api/comics';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Comic[]>(this.API);
  }

  getById(id: string) {
    return this.http.get<Comic>(`${this.API}/${id}`);
  }

  create(comic: SaveComic) {
    return this.http.post<Comic>(this.API, comic);
  }

  update(id: string, comic: SaveComic) {
    return this.http.put<Comic>(`${this.API}/${id}`, comic);
  }

  delete(id: string) {
    return this.http.delete<void>(`${this.API}/${id}`);
  }
}