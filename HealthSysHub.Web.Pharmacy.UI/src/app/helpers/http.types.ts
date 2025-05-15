// http.types.ts
export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH' | 'HEAD' | 'OPTIONS';
export type BodylessMethod = 'GET' | 'HEAD' | 'DELETE' | 'OPTIONS';
export type BodyMethod = Exclude<HttpMethod, BodylessMethod>;