export interface User {
    id: string;
    username: string;
    email: string;
    role: string;
}

export interface AuthResponse {
    token: string;
}
