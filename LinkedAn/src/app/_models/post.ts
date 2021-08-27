import { User } from "./user";

export class Post {
    id: number;
    titulo: string;
    conteudo: string;
    data: string;
    applicationUserId: string;
    applicationUser: User;
    qtdLikes: number;
}