
```
ECommerceNew
├─ backend
│  ├─ .dockerignore
│  ├─ Dockerfile
│  ├─ ECommerceNew
│  │  ├─ appsettings.Development.json
│  │  ├─ appsettings.json
│  │  ├─ Controllers
│  │  │  ├─ AuthenticationController.cs
│  │  │  └─ ProductsController.cs
│  │  ├─ ECommerceNew.Api.csproj
│  │  ├─ ECommerceNew.http
│  │  ├─ ECommerceNew.sln
│  │  ├─ Program.cs
│  │  └─ Properties
│  │     └─ launchSettings.json
│  ├─ ECommerceNew.Application
│  │  ├─ Abstractions
│  │  │  ├─ IEmailService.cs
│  │  │  ├─ IProductRepository.cs
│  │  │  ├─ IStorageRepoistory.cs
│  │  │  ├─ ITokenService.cs
│  │  │  └─ IUserRepository.cs
│  │  ├─ Auth
│  │  │  ├─ Commands
│  │  │  │  ├─ EmailVerification
│  │  │  │  │  ├─ VerificationCommand.cs
│  │  │  │  │  ├─ VerificationHandler.cs
│  │  │  │  │  └─ VerificationValidator.cs
│  │  │  │  ├─ PasswordRecoveryCode
│  │  │  │  │  ├─ PasswordRecoveryCommand.cs
│  │  │  │  │  ├─ PasswordRecoveryHandler.cs
│  │  │  │  │  └─ PasswordRecoveryValidator.cs
│  │  │  │  ├─ PasswordReset
│  │  │  │  │  ├─ PasswordResetCommand.cs
│  │  │  │  │  ├─ PasswordResetHandler.cs
│  │  │  │  │  └─ PasswordResetValidator.cs
│  │  │  │  ├─ UserLogin
│  │  │  │  │  ├─ LoginCommand.cs
│  │  │  │  │  ├─ LoginHandler.cs
│  │  │  │  │  └─ LoginValidator.cs
│  │  │  │  ├─ UserRegister
│  │  │  │  │  ├─ RegisterCommand.cs
│  │  │  │  │  ├─ RegisterHandler.cs
│  │  │  │  │  └─ RegisterValidator.cs
│  │  │  │  └─ VerifyRecoveryCode
│  │  │  │     ├─ VerifyRecoveryCodeCommand.cs
│  │  │  │     ├─ VerifyRecoveryCodeHandler.cs
│  │  │  │     └─ VerifyRecoveryCodeValidator.cs
│  │  │  └─ DTOs
│  │  │     ├─ EmailVerificationDto.cs
│  │  │     ├─ LoginRequest.cs
│  │  │     ├─ PasswordRecoveryEmailDto.cs
│  │  │     ├─ PasswordResetDto.cs
│  │  │     ├─ RecoveryCodeVerificationDto.cs
│  │  │     └─ RegisterRequest.cs
│  │  ├─ DependenyInjection.cs
│  │  ├─ ECommerceNew.Application.csproj
│  │  ├─ Exceptions
│  │  │  ├─ BaseExceptionClass.cs
│  │  │  ├─ ErrorResponse.cs
│  │  │  ├─ GlobalExceptionHandlingMiddleware.cs
│  │  │  └─ ValidationException.cs
│  │  └─ ProductCQRS
│  │     ├─ Commands
│  │     │  ├─ AddToCart
│  │     │  │  ├─ AddToCartCommand.cs
│  │     │  │  ├─ AddToCartHandler.cs
│  │     │  │  └─ AddToCartValidator.cs
│  │     │  ├─ AddToWishList
│  │     │  │  ├─ AddToWishListCommand.cs
│  │     │  │  ├─ AddToWishListHandler.cs
│  │     │  │  └─ AddToWishListValidator.cs
│  │     │  ├─ CreateProduct
│  │     │  │  ├─ CreateProductCommand.cs
│  │     │  │  ├─ CreateProductHandler.cs
│  │     │  │  └─ CreateProductValidator.cs
│  │     │  ├─ DeleteProduct
│  │     │  │  ├─ DeleteProductCommand.cs
│  │     │  │  ├─ DeleteProductHandler.cs
│  │     │  │  └─ DeleteProductValidator.cs
│  │     │  ├─ DeleteProductImage
│  │     │  │  ├─ DeleteProductImageCommand.cs
│  │     │  │  ├─ DeleteProductImageHandler.cs
│  │     │  │  └─ DeleteProductImageValidator.cs
│  │     │  ├─ RemoveFromCart
│  │     │  │  ├─ RemoveFromCartCommand.cs
│  │     │  │  ├─ RemoveFromCartHandler.cs
│  │     │  │  └─ RemoveFromCartValidator.cs
│  │     │  ├─ RemoveFromWishlist
│  │     │  │  ├─ RemoveFromWishlistCommand.cs
│  │     │  │  ├─ RemoveFromWishlistHandler.cs
│  │     │  │  └─ RemoveFromWishlistValidator.cs
│  │     │  ├─ UpdateProduct
│  │     │  │  ├─ UpdateProductCommand.cs
│  │     │  │  ├─ UpdateProductHandler.cs
│  │     │  │  └─ UpdateProductValidator.cs
│  │     │  └─ UploadImage
│  │     │     ├─ UploadImageCommand.cs
│  │     │     ├─ UploadImageHandler.cs
│  │     │     └─ UploadImageValidator.cs
│  │     ├─ DTOs
│  │     │  └─ ProductDtos
│  │     │     ├─ AddToCartDto.cs
│  │     │     ├─ AddToWishlistDto.cs
│  │     │     ├─ CartItemDetailDto.cs
│  │     │     ├─ CartItemsQueryParameters.cs
│  │     │     ├─ CreateProductDto.cs
│  │     │     ├─ DeleteProductDto.cs
│  │     │     ├─ DeleteProductImageDto.cs
│  │     │     ├─ PagedResult.cs
│  │     │     ├─ ProductDetailDto.cs
│  │     │     ├─ ProductImagesDto.cs
│  │     │     ├─ ProductQueryParameters.cs
│  │     │     ├─ RemoveFromCartDto.cs
│  │     │     ├─ RemoveFromWishlistDto.cs
│  │     │     ├─ UpdateProductDto.cs
│  │     │     ├─ UploadImageDto.cs
│  │     │     ├─ WishListDetailDto.cs
│  │     │     └─ WishlistQueryParameters.cs
│  │     └─ Queries
│  │        ├─ GetAllProducts
│  │        │  ├─ GetAllProductsQuery.cs
│  │        │  └─ GetAllProductsQueryHandler.cs
│  │        ├─ GetCartItems
│  │        │  ├─ GetCartItemsHandler.cs
│  │        │  └─ GetCartItemsQuery.cs
│  │        ├─ GetImagePreSignedUrl
│  │        │  ├─ GetPreSignedUrlQuery.cs
│  │        │  └─ GetPreSignedUrlQueryHandler.cs
│  │        ├─ GetProductById
│  │        │  ├─ GetProductByIdQuery.cs
│  │        │  └─ GetProductByIdQueryHandler.cs
│  │        └─ GetWishList
│  │           ├─ GetWishListQuery.cs
│  │           └─ GetWishListQueryHandler.cs
│  ├─ ECommerceNew.Domain
│  │  ├─ ECommerceNew.Domain.csproj
│  │  └─ Entities
│  │     ├─ ProductSide
│  │     │  ├─ Cart.cs
│  │     │  ├─ CartItem.cs
│  │     │  ├─ Product.cs
│  │     │  ├─ ProductCategory.cs
│  │     │  ├─ ProductImage.cs
│  │     │  ├─ Review.cs
│  │     │  └─ WishListItem.cs
│  │     ├─ StoreInfo
│  │     │  └─ StoreInfo.cs
│  │     └─ UserSide
│  │        ├─ EmailVerification.cs
│  │        ├─ Role.cs
│  │        └─ User.cs
│  ├─ ECommerceNew.Infrastructure
│  │  ├─ DependencyInjection.cs
│  │  ├─ ECommerceNew.Infrastructure.csproj
│  │  ├─ EfCore
│  │  │  └─ DbContext.cs
│  │  ├─ Migrations
│  │  │  ├─ 20260228180238_InitialMigration.cs
│  │  │  ├─ 20260228180238_InitialMigration.Designer.cs
│  │  │  ├─ 20260307141906_AddedSomeTables.cs
│  │  │  ├─ 20260307141906_AddedSomeTables.Designer.cs
│  │  │  ├─ 20260307145813_ChangedSchemaABit003.cs
│  │  │  ├─ 20260307145813_ChangedSchemaABit003.Designer.cs
│  │  │  ├─ 20260314141620_RemovedPaymentTables.cs
│  │  │  ├─ 20260314141620_RemovedPaymentTables.Designer.cs
│  │  │  ├─ 20260314142521_fixedProductCategories.cs
│  │  │  ├─ 20260314142521_fixedProductCategories.Designer.cs
│  │  │  ├─ 20260314142613_fixedProductCategoriesSecondVersion.cs
│  │  │  ├─ 20260314142613_fixedProductCategoriesSecondVersion.Designer.cs
│  │  │  ├─ 20260314142816_AddedParentCategories.cs
│  │  │  ├─ 20260314142816_AddedParentCategories.Designer.cs
│  │  │  ├─ 20260315124815_RemovedPhoneNumber.cs
│  │  │  ├─ 20260315124815_RemovedPhoneNumber.Designer.cs
│  │  │  ├─ 20260322140829_SeedProductCategories.cs
│  │  │  ├─ 20260322140829_SeedProductCategories.Designer.cs
│  │  │  ├─ 20260322141445_AddWishListItems.cs
│  │  │  ├─ 20260322141445_AddWishListItems.Designer.cs
│  │  │  ├─ 20260325131907_AddAdditionDateToWishlistEntity.cs
│  │  │  ├─ 20260325131907_AddAdditionDateToWishlistEntity.Designer.cs
│  │  │  ├─ 20260330135212_FixWishlistAndCart.cs
│  │  │  ├─ 20260330135212_FixWishlistAndCart.Designer.cs
│  │  │  ├─ 20260330140706_AddQuantityToWishlist.cs
│  │  │  ├─ 20260330140706_AddQuantityToWishlist.Designer.cs
│  │  │  └─ ECommerceDbContextModelSnapshot.cs
│  │  ├─ Repositories
│  │  │  ├─ ProductRepository.cs
│  │  │  ├─ SmtpEmailService.cs
│  │  │  ├─ StorageRepository.cs
│  │  │  ├─ TokenService.cs
│  │  │  └─ UserRepository.cs
│  │  └─ UserRolesEnum.cs
│  └─ ECommerceNew.sln
├─ docker-compose.yml
├─ Dockerfile
├─ frontend
│  ├─ css
│  │  └─ styles.css
│  ├─ Dockerfile
│  ├─ ekkoShopFavicon.png
│  ├─ forgot-password.html
│  ├─ index.html
│  ├─ js
│  │  ├─ api.js
│  │  ├─ applyI18n.js
│  │  ├─ auth
│  │  │  ├─ forgotPasswordPage.js
│  │  │  ├─ loginPage.js
│  │  │  ├─ registerPage.js
│  │  │  └─ verifyEmailPage.js
│  │  ├─ auth.js
│  │  ├─ categories.js
│  │  ├─ i18n.js
│  │  ├─ store.js
│  │  └─ ui.js
│  ├─ login.html
│  ├─ register.html
│  ├─ serve.js
│  └─ verify-email.html
└─ ServicePorts.txt

```