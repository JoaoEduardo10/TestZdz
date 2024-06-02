export interface ApiResponseError<T> {
  Success: boolean;
  Result: T;
  Message: string;
  ErrorMessage: {
    Code: string;
    Message: string;
  };
}
