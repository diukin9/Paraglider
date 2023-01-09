export enum AuthProvider {
  Yandex = "Yandex",
  Vkontakte = "Vkontakte",
}

export interface RegisterUserRequest {
  email: string;
  password: string;
  firstName: string;
  surname: string;
  phoneNumber?: string | undefined;
  cityId: string;
}

export interface BasicAuthRequest {
  login: string;
  password: string;
}

export interface User {
  id: string;
  firstName: string;
  surname: string;
  username: string;
  email?: string;
  city: City;
  planning: Planning;
  favourites: UserComponent[];
}

export interface City {
  id: string;
  name: string;
}

export interface Planning {
  weddingDate: string;
  categories: Category[];
  components: PlanningComponent[];
}

export interface Category {
  id: string;
  name: string;
}

export interface PlanningComponent {
  componentId: string;
  component?: Component;
  componentDesc?: ComponentDesc;
}

export interface Component {
  id: string;
  category: Category;
  name: string;
  description?: string;
  avatarUrl?: string;
  city: City;
  album: Album;
  contacts: Contacts;
  reviews: Review[];
  manufactureYear?: string;
  minRentLength?: string;
  capacity?: number;
  services?: Service[];
  halls?: Hall[];
}

export interface Album {
  name?: string;
  media: Media[];
}

export interface Media {
  type: MediaType;
  url: string;
}

export interface MediaType {
  name: string;
  value: number;
}

export interface Contacts {
  phoneNumber: string;
  email?: string;
  telegram?: string;
  whatsApp?: string;
  viber?: string;
  vkontakte?: string;
  instagram?: string;
}

export interface Review {
  author: string;
  avatarUrl: string;
  text?: string;
  evaluation: number;
}

export interface Service {
  name: string;
  description?: string;
  price: Price;
}

export interface Price {
  min?: number;
  max?: number;
}

export interface Hall {
  id: string;
  name: string;
  description?: string;
  rentalPrice: HallRentalPrice;
  capacity: Capacity;
  album: Album;
  minimalPrice?: number;
}

export interface HallRentalPrice {
  pricePerPerson?: number;
  rentalPrice?: number;
}

export interface Capacity {
  min?: number;
  max?: number;
}

export interface ComponentDesc {
  status: ComponentStatus;
  timeInterval: TimeInterval;
  payments: Payment[];
}

export interface ComponentStatus {
  name: string;
  value: number;
}

export interface TimeInterval {
  intervalStart?: string;
  intervalEnd?: string;
}

export interface Payment {
  id: string;
  date: string;
  status: PaymentStatus;
  sum?: number;
}

export interface PaymentStatus {
  name: string;
  value: number;
}

export interface UserComponent {
  componentId: string;
  component?: Component;
}
