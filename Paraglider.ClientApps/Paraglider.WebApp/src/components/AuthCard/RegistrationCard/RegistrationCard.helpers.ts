import { RegisterUserRequest } from "../../../typings/server";

export interface RegistrationFrom extends RegisterUserRequest {
  repeatPassword: string;
}
