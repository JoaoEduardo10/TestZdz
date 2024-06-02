export interface ApiResponse<T> {
  success: boolean;
  result: T;
  message: string;
  errorMessage: {
    code: string;
    message: string;
  };
}
