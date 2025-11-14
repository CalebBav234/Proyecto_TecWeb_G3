# TODO: Implement Profile CRUD Operations

## Steps to Complete

- [x] Create IProfileRepository interface with methods: GetByIdAsync(int id), GetByUserIdAsync(Guid userId), GetPagedAsync(int page, int pageSize), AddAsync(Profile profile), UpdateAsync(Profile profile), RemoveAsync(Profile profile).
- [x] Create ProfileRepository implementing IProfileRepository, using AppDbContext.
- [x] Create IProfileService interface with methods: GetByIdAsync(int id), GetByUserIdAsync(Guid userId), GetPagedAsync(int page, int pageSize), CreateAsync(CreateProfileDto dto, Guid userId), UpdateAsync(int id, UpdateProfileDto dto, Guid userId), DeleteAsync(int id, Guid userId).
- [x] Create ProfileService implementing IProfileService, using repository and mapper.
- [x] Create ProfileController with endpoints: GET /{userId} (own or admin), GET /all (admin with pagination using GetPagedAsync), POST / (create for own user), PUT /{id} (own or admin), DELETE /{id} (own or admin), with authorization.
- [x] Update Program.cs to register IProfileRepository, ProfileRepository, IProfileService, ProfileService in DI.
- [ ] Test the new endpoints to ensure they work correctly.
