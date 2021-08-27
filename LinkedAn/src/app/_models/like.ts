import { Post } from "./post";
import { User } from "./user";

export class Like {
    id: number;
    applicationUserId: string;
    applicationUser: User;
    data: string;
    post: Post;
    postId: number;
}