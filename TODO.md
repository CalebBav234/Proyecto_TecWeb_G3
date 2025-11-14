# TODO: Update AuthService to Match Example

## Steps to Complete

- [x] Update User.cs: Change Id to Guid, add Username, change PasswordHash to string, remove PasswordSalt, add RefreshToken, RefreshTokenExpiresAt, RefreshTokenRevokedAt, CurrentJwtId.
- [x] Update IUserRepository.cs: Add GetByRefreshToken(string refreshToken), change GetByIdAsync to Guid id.
- [x] Update UserRepository.cs: Implement GetByRefreshToken, update GetByIdAsync to Guid, update includes.
- [x] Update RegisterUserDto.cs: Add Username, Role.
- [x] Create LoginResponseDto.cs in DTOs/Dtos/.
- [x] Create RefreshRequestDto.cs in DTOs/Dtos/.
- [x] Update IAuthService.cs: Change methods to match example (RegisterAsync Task<string>, LoginAsync Task<(bool, LoginResponseDto?)>, add RefreshAsync, LogoutAsync).
- [x] Update AuthService.cs: Implement new logic with BCrypt, JWT with JTI, refresh tokens, adapt to Guid Id.
- [x] Update AppDbContext.cs: Change User Id to Guid.
- [x] Install BCrypt.Net-Next package.
- [ ] Run EF migrations for DB changes.
- [ ] Test auth endpoints.
