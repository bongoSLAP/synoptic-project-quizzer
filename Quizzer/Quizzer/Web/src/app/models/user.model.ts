import { Role } from "./role.model";

export class User {
    public id: string;
    public firstName: string;
    public lastName: string;
    public email: string;
    public role: Role;
}
