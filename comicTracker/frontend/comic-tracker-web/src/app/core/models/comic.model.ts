export interface Comic {
  id: string;
  title: string;
  writer: string;
  artist: string;
  publisher: string;  
  readed: boolean;
  createdAt: string;
}

export interface SaveComic {
  title: string;
  writer: string;
  artist: string;
  publisher: string;  
  readed: boolean;
}