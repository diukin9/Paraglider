import { Link } from "../../components/Link";
import {
  ErrorBoundaryCard,
  ErrorBoundaryRoot,
  ErrorBoundaryTitle,
  ErrorDescription,
} from "./ErrorBoundary.styles";

export const ErrorBoundary = () => {
  return (
    <ErrorBoundaryRoot>
      <ErrorBoundaryCard>
        <ErrorBoundaryTitle>Ошибка&nbsp;404</ErrorBoundaryTitle>
        <ErrorDescription>Страницы по&nbsp;указанному адресу не&nbsp;существует</ErrorDescription>
        <Link to="/">Вернуться на главную </Link>
      </ErrorBoundaryCard>
    </ErrorBoundaryRoot>
  );
};
