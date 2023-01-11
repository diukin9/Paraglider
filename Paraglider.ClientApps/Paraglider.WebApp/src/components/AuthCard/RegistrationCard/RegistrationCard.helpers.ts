import { RegisterUserRequest } from "../../../typings/server";

export interface RegistrationForm extends RegisterUserRequest {
  repeatPassword: string;
}
