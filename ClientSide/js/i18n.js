/* ══════════════════════════════════════
   i18n.js — Georgian ↔ English translations
   Usage: t('key') returns current language string
   Only affects UI — product names/descriptions untouched
══════════════════════════════════════ */

export const TRANSLATIONS = {

  /* ── Header ── */
  'search.placeholder':     { ka: 'მოძებნე პროდუქტი, კატეგორია…',  en: 'Search products, categories…' },
  'header.cart':            { ka: 'კალათა',                          en: 'Cart' },
  'header.login':           { ka: 'შესვლა',                          en: 'Log In' },
  'header.signup':          { ka: 'რეგისტრაცია',                     en: 'Sign Up' },
  'header.logout':          { ka: '↩ გასვლა',                        en: '↩ Log Out' },
  'header.adminPanel':      { ka: '⚙ ადმინ პანელი',                  en: '⚙ Admin Panel' },

  /* ── Hero ── */
  'hero.tag':               { ka: 'ახალი ჩამოსვლები 2025',           en: 'New Arrivals 2025' },
  'hero.heading1':          { ka: 'ტექნოლოგია, რომელიც',             en: 'Tech that' },
  'hero.heading2':          { ka: 'გადადგამს შენ წინ',               en: 'moves your world forward' },
  'hero.sub':               { ka: 'აღმოაჩინე უახლესი ელექტრონიკა, ლეპტოპები, სმარტფონები და მეტი — სწრაფი მიწოდებით.', en: 'Discover cutting-edge electronics, laptops, smartphones and more — shipped fast to your door.' },
  'hero.shopNow':           { ka: 'იყიდე ახლა →',                   en: 'Shop Now →' },
  'hero.browse':            { ka: 'კატეგორიები',                     en: 'Browse Categories' },
  'hero.float1.title':      { ka: 'RTX 5080',                        en: 'RTX 5080' },
  'hero.float1.sub':        { ka: 'ახლახან მოვიდა',                  en: 'Just arrived' },
  'hero.float2.title':      { ka: 'სწრაფი მიწოდება',                 en: 'Fast Delivery' },
  'hero.float2.sub':        { ka: 'იმავე დღეს',                      en: 'Same day' },
  'hero.float3.title':      { ka: '2 წლის გარანტია',                 en: '2 Year Warranty' },
  'hero.float3.sub':        { ka: 'ყველა პროდუქტზე',                 en: 'All products' },

  /* ── Category section ── */
  'categories.title':       { ka: 'კატეგორიები',                     en: 'Browse by Category' },
  'cat.subcategories':      { ka: 'ქვეკატეგორია',                    en: 'subcategories' },
  'cat.allItems':           { ka: 'ყველა პროდუქტი',                  en: 'All items' },
  'cat.all':                { ka: 'ყველა',                           en: 'All' },

  /* ── Category names (matches DB) ── */
  'cat.Gaming Consoles & Accessories': { ka: 'სათამაშო კონსოლები და აქსესუარები', en: 'Gaming Consoles & Accessories' },
  'cat.PC Hardware & Components':      { ka: 'PC აპარატურა და კომპონენტები',      en: 'PC Hardware & Components' },
  'cat.PC & Gaming Peripherals':       { ka: 'PC და გეიმინგ პერიფერიები',          en: 'PC & Gaming Peripherals' },
  'cat.Gaming PCs & Laptops':          { ka: 'სათამაშო PC და ლეპტოპები',          en: 'Gaming PCs & Laptops' },
  'cat.Services':                      { ka: 'სერვისები',                          en: 'Services' },
  'cat.Consoles':                      { ka: 'კონსოლები',                          en: 'Consoles' },
  'cat.Gamepads & Controllers':        { ka: 'გეიმპადები და კონტროლერები',        en: 'Gamepads & Controllers' },
  'cat.Controller Accessories':        { ka: 'კონტროლერის აქსესუარები',           en: 'Controller Accessories' },
  'cat.Gift Cards / Subscriptions':    { ka: 'სასაჩუქრე ბარათები / სერვისები',   en: 'Gift Cards / Subscriptions' },
  'cat.Processors (CPU)':              { ka: 'პროცესორები (CPU)',                  en: 'Processors (CPU)' },
  'cat.Graphics Cards (GPU)':          { ka: 'ვიდეო ბარათები (GPU)',              en: 'Graphics Cards (GPU)' },
  'cat.Motherboards':                  { ka: 'დედა დაფები',                        en: 'Motherboards' },
  'cat.Memory (RAM)':                  { ka: 'ოპერატიული მეხსიერება (RAM)',       en: 'Memory (RAM)' },
  'cat.Storage (SSD / HDD / NVMe)':   { ka: 'მეხსიერება (SSD / HDD / NVMe)',     en: 'Storage (SSD / HDD / NVMe)' },
  'cat.Power Supplies (PSU)':          { ka: 'კვების ბლოკები (PSU)',              en: 'Power Supplies (PSU)' },
  'cat.PC Cases':                      { ka: 'კომპიუტერის კორპუსები',             en: 'PC Cases' },
  'cat.Cooling Solutions':             { ka: 'გაგრილების სისტემები',              en: 'Cooling Solutions' },
  'cat.Mining / Crypto Gear':          { ka: 'მაინინგი / კრიპტო',                en: 'Mining / Crypto Gear' },
  'cat.Monitors':                      { ka: 'მონიტორები',                         en: 'Monitors' },
  'cat.Keyboards':                     { ka: 'კლავიატურები',                       en: 'Keyboards' },
  'cat.Mice':                          { ka: 'მაუსები',                            en: 'Mice' },
  'cat.Headsets':                      { ka: 'ყურსასმენები',                       en: 'Headsets' },
  'cat.Speakers':                      { ka: 'დინამიკები',                         en: 'Speakers' },
  'cat.Other Accessories':             { ka: 'სხვა აქსესუარები',                  en: 'Other Accessories' },
  'cat.Desktop PCs':                   { ka: 'სტაციონარული კომპიუტერები',         en: 'Desktop PCs' },
  'cat.Gaming Laptops':                { ka: 'სათამაშო ლეპტოპები',                en: 'Gaming Laptops' },
  'cat.Laptop Accessories':            { ka: 'ლეპტოპის აქსესუარები',             en: 'Laptop Accessories' },
  'cat.PC / Laptop Assembly':          { ka: 'PC / ლეპტოპის აწყობა',             en: 'PC / Laptop Assembly' },
  'cat.PC / Laptop Repair':            { ka: 'PC / ლეპტოპის შეკეთება',           en: 'PC / Laptop Repair' },
  'cat.Upgrades & Custom Builds':      { ka: 'განახლება და Custom Build',         en: 'Upgrades & Custom Builds' },
  'cat.Data Recovery':                 { ka: 'მონაცემების აღდგენა',               en: 'Data Recovery' },
  'cat.Consultation & Troubleshooting':{ ka: 'კონსულტაცია და პრობლემების მოგვარება', en: 'Consultation & Troubleshooting' },

  /* ── Shop / filters ── */
  'shop.allProducts':       { ka: 'ყველა პროდუქტი',                  en: 'All Products' },
  'shop.products':          { ka: 'პროდუქტი',                        en: 'product' },
  'shop.productsPlural':    { ka: 'პროდუქტი',                        en: 'products' },
  'filter.allCategories':   { ka: '— ყველა კატეგორია —',             en: '— All Categories —' },
  'filter.allStock':        { ka: 'მარაგი',                          en: 'All Stock' },
  'filter.inStock':         { ka: 'მარაგშია',                        en: 'In Stock' },
  'filter.outOfStock':      { ka: 'არ არის მარაგში',                 en: 'Out of Stock' },
  'filter.minPrice':        { ka: 'მინ ₾',                           en: 'Min ₾' },
  'filter.maxPrice':        { ka: 'მაქს ₾',                          en: 'Max ₾' },
  'filter.apply':           { ka: 'გაფილტვრა',                       en: 'Filter' },
  'filter.clear':           { ka: 'გასუფთავება',                     en: 'Clear' },

  /* ── Product card ── */
  'product.view':           { ka: 'ნახვა',                           en: 'View' },
  'product.inStock':        { ka: 'მარაგშია',                        en: 'In Stock' },
  'product.outOfStock':     { ka: 'არ არის მარაგში',                 en: 'Out of Stock' },
  'product.addToCart':      { ka: '🛒 კალათაში დამატება',            en: '🛒 Add to Cart' },
  'product.wishlist':       { ka: '♡ სურვილების სია',                en: '♡ Wishlist' },
  'product.id':             { ka: 'პროდუქტის ID',                    en: 'Product ID' },
  'product.category':       { ka: 'კატეგორია',                       en: 'Category' },
  'product.added':          { ka: 'დამატებულია',                     en: 'Added' },
  'product.notFound':       { ka: 'პროდუქტი ვერ მოიძებნა',          en: 'Product not found' },

  /* ── Empty/loading ── */
  'products.empty':         { ka: '😔 ფილტრით პროდუქტი ვერ მოიძებნა.', en: '😔 No products found matching your filters.' },
  'products.clearFilters':  { ka: 'ფილტრის გასუფთავება',             en: 'Clear Filters' },
  'products.loading':       { ka: 'იტვირთება…',                      en: 'Loading…' },
  'products.error':         { ka: 'პროდუქტების ჩატვირთვა ვერ მოხერხდა. API მუშაობს?', en: 'Could not load products. Is the API running?' },

  /* ── Pagination ── */
  'page.prev':              { ka: '← წინა',                          en: '← Prev' },
  'page.next':              { ka: 'შემდეგი →',                       en: 'Next →' },

  /* ── Admin overlay ── */
  'admin.addImage':         { ka: '⬆ დამატება',                      en: '⬆ Add' },
  'admin.delImage':         { ka: '🗑 წაშლა',                        en: '🗑 Del' },
  'admin.confirmDelImg':    { ka: 'წაიშალოს ეს სურათი?',            en: 'Delete this image?' },
  'admin.confirmDelProd':   { ka: 'სამუდამოდ წაიშალოს პროდუქტი?',   en: 'Permanently delete this product?' },
  'admin.deleteFailed':     { ka: 'წაშლა ვერ მოხერხდა',             en: 'Delete failed' },
  'admin.imageDeleted':     { ka: 'სურათი წაიშალა',                  en: 'Image deleted' },
  'admin.addImageBtn':      { ka: '⬆ სურათის დამატება',             en: '⬆ Add Image' },
  'admin.deleteProduct':    { ka: '🗑 პროდუქტის წაშლა',             en: '🗑 Delete Product' },
  'admin.editProduct':      { ka: '✏ პროდუქტის რედაქტირება',        en: '✏ Edit Product' },
  'admin.show':             { ka: 'ჩვენება',                         en: 'Show' },
  'admin.hide':             { ka: 'დამალვა',                         en: 'Hide' },
  'admin.saveChanges':      { ka: '✓ ცვლილებების შენახვა',           en: '✓ Save Changes' },
  'admin.saved':            { ka: '✓ შენახულია!',                    en: '✓ Saved!' },
  'admin.updated':          { ka: 'პროდუქტი განახლდა',               en: 'Product updated' },

  /* ── Admin drawer ── */
  'drawer.title':           { ka: '⚙ ადმინ პანელი',                  en: '⚙ Admin Panel' },
  'drawer.createProduct':   { ka: 'ახალი პროდუქტის შექმნა',          en: 'Create New Product' },
  'drawer.name':            { ka: 'პროდუქტის სახელი *',              en: 'Product name *' },
  'drawer.price':           { ka: 'ფასი (₾)',                         en: 'Price (₾)' },
  'drawer.amount':          { ka: 'რაოდენობა',                       en: 'Amount in stock' },
  'drawer.description':     { ka: 'აღწერა',                          en: 'Description' },
  'drawer.selectCat':       { ka: '— კატეგორიის არჩევა —',           en: '— Select category —' },
  'drawer.attachImage':     { ka: '📷 სურათის დამატება (სურვილისამებრ)', en: '📷 Attach image (optional)' },
  'drawer.create':          { ka: '✦ პროდუქტის შექმნა',              en: '✦ Create Product' },
  'drawer.creating':        { ka: 'იქმნება…',                        en: 'Creating…' },
  'drawer.created':         { ka: '✓ შეიქმნა! ID:',                  en: '✓ Created! ID:' },
  'drawer.quickDelete':     { ka: 'სწრაფი წაშლა',                    en: 'Quick Delete' },
  'drawer.productId':       { ka: 'პროდუქტის ID',                    en: 'Product ID' },
  'drawer.deleteBtn':       { ka: '🗑 პროდუქტის წაშლა',             en: '🗑 Delete Product' },
  'drawer.nameRequired':    { ka: '✕ სახელი სავალდებულოა',           en: '✕ Name is required' },
  'drawer.enterIdFirst':    { ka: '✕ შეიყვანე პროდუქტის ID',        en: '✕ Enter a product ID' },

  /* ── Upload modal ── */
  'upload.title':           { ka: 'სურათის ატვირთვა',                en: 'Upload Image' },
  'upload.product':         { ka: 'პროდუქტი #',                      en: 'Product #' },
  'upload.drop':            { ka: 'ჩააგდე სურათი ან დააჭირე',       en: 'Drop an image or click to browse' },
  'upload.hint':            { ka: 'JPG · PNG · WEBP — მაქს 10MB',   en: 'JPG · PNG · WEBP — max 10MB' },
  'upload.btn':             { ka: '⬆ ატვირთვა',                      en: '⬆ Upload' },
  'upload.cancel':          { ka: 'გაუქმება',                        en: 'Cancel' },
  'upload.uploading':       { ka: '↻ იტვირთება…',                    en: '↻ Uploading…' },
  'upload.success':         { ka: '✓ სურათი ატვირთულია!',            en: '✓ Image uploaded!' },
  'upload.toasted':         { ka: 'სურათი ატვირთულია',               en: 'Image uploaded' },
  'upload.failed':          { ka: 'ატვირთვა ვერ მოხერხდა',           en: 'Upload failed' },
  'upload.dropOnly':        { ka: 'გთხოვ ჩააგდე მხოლოდ სურათი',    en: 'Please drop an image file' },

  /* ── Cart coming soon ── */
  'cart.title':             { ka: 'კალათა მალე გამოჩნდება',          en: 'Cart is Coming Soon' },
  'cart.sub':               { ka: 'ვმუშაობთ მოსახერხებელ შეძენის გამოცდილებაზე. მალე!', en: "We're working hard to bring you a seamless checkout experience. Stay tuned!" },
  'cart.close':             { ka: 'დახურვა',                         en: 'Close' },
  'cart.progress':          { ka: '65% დასრულებულია',                en: '65% complete' },

  /* ── Footer ── */
  'footer.tagline':         { ka: 'საქართველოს საყვარელი ონლაინ ტექ-მაღაზია.\nხარისხიანი პროდუქტი, სწრაფი მიწოდება.', en: "Georgia's favourite online tech store.\nQuality products, fast delivery." },
  'footer.shop':            { ka: 'მაღაზია',                         en: 'Shop' },
  'footer.help':            { ka: 'დახმარება',                       en: 'Help' },
  'footer.company':         { ka: 'კომპანია',                        en: 'Company' },
  'footer.contact':         { ka: 'კონტაქტი',                        en: 'Contact Us' },
  'footer.returns':         { ka: 'დაბრუნება',                       en: 'Returns' },
  'footer.shipping':        { ka: 'მიწოდება',                        en: 'Shipping Info' },
  'footer.faq':             { ka: 'კითხვები',                        en: 'FAQ' },
  'footer.about':           { ka: 'ჩვენ შესახებ',                   en: 'About' },
  'footer.careers':         { ka: 'ვაკანსიები',                      en: 'Careers' },
  'footer.press':           { ka: 'პრესა',                           en: 'Press' },
  'footer.blog':            { ka: 'ბლოგი',                           en: 'Blog' },
  'footer.rights':          { ka: '© 2025 EkkoShop. ყველა უფლება დაცულია.', en: '© 2025 EkkoShop. All rights reserved.' },
  'footer.made':            { ka: 'გაკეთებულია სიყვარულით საქართველოში 🇬🇪', en: 'Made with ♥ in Georgia 🇬🇪' },

  /* ── Login page ── */
  'login.title':            { ka: 'შესვლა',                          en: 'Log In' },
  'login.subtitle':         { ka: 'EkkoShop-ზე ახალი ხარ?',         en: 'New to EkkoShop?' },
  'login.create':           { ka: 'ანგარიშის შექმნა →',             en: 'Create an account →' },
  'login.email':            { ka: 'ელ-ფოსტა',                       en: 'Email Address' },
  'login.password':         { ka: 'პაროლი',                          en: 'Password' },
  'login.forgot':           { ka: 'დაგავიწყდა პაროლი?',             en: 'Forgot password?' },
  'login.btn':              { ka: 'შესვლა',                          en: 'Log In' },
  'login.loading':          { ka: 'მიმდინარეობს შესვლა…',            en: 'Logging in…' },
  'login.or':               { ka: 'ან გააგრძელე',                    en: 'or continue with' },
  'login.terms':            { ka: 'შესვლით ეთანხმები ჩვენს',        en: 'By logging in you agree to our' },
  'login.tos':              { ka: 'სარგებლობის პირობებს',            en: 'Terms of Service' },
  'login.privacy':          { ka: 'კონფიდენციალურობის პოლიტიკას',   en: 'Privacy Policy' },
  'login.welcome':          { ka: 'კეთილი იყოს შენი დაბრუნება!',    en: 'Welcome back!' },
  'login.visual.h2':        { ka: 'კეთილი იყოს შენი დაბრუნება.',    en: 'Welcome back.' },
  'login.visual.p':         { ka: 'შედი და ნახე შენი შეკვეთები, სურვილების სია და განსაკუთრებული შეთავაზებები.', en: 'Log in to access your orders, wishlist and exclusive member deals.' },
  'login.stat1.num':        { ka: '12k+',                            en: '12k+' },
  'login.stat1.lbl':        { ka: 'პროდუქტი',                       en: 'Products' },
  'login.stat2.num':        { ka: '99%',                             en: '99%' },
  'login.stat2.lbl':        { ka: 'კმაყოფილი მომხმარებელი',         en: 'Happy Customers' },
  'login.stat3.num':        { ka: '24სთ',                            en: '24h' },
  'login.stat3.lbl':        { ka: 'მიწოდება',                       en: 'Delivery' },

  /* ── Register page ── */
  'reg.title':              { ka: 'ანგარიშის შექმნა',                en: 'Create Account' },
  'reg.subtitle':           { ka: 'უკვე გაქვს ანგარიში?',           en: 'Already have one?' },
  'reg.login':              { ka: 'შესვლა →',                        en: 'Log in →' },
  'reg.firstName':          { ka: 'სახელი',                          en: 'First Name' },
  'reg.lastName':           { ka: 'გვარი',                           en: 'Last Name' },
  'reg.email':              { ka: 'ელ-ფოსტა',                       en: 'Email Address' },
  'reg.phone':              { ka: 'ტელეფონი',                        en: 'Phone Number' },
  'reg.optional':           { ka: '(სურვილისამებრ)',                 en: '(optional)' },
  'reg.password':           { ka: 'პაროლი',                          en: 'Password' },
  'reg.confirm':            { ka: 'პაროლის დადასტურება',             en: 'Confirm Password' },
  'reg.terms':              { ka: 'ვეთანხმები EkkoShop-ის',          en: "I agree to EkkoShop's" },
  'reg.tos':                { ka: 'სარგებლობის პირობებს',            en: 'Terms of Service' },
  'reg.and':                { ka: 'და',                              en: 'and' },
  'reg.privacy':            { ka: 'კონფიდენციალურობის პოლიტიკას',   en: 'Privacy Policy' },
  'reg.btn':                { ka: 'ანგარიშის შექმნა',                en: 'Create Account' },
  'reg.loading':            { ka: 'იქმნება ანგარიში…',              en: 'Creating account…' },
  'reg.footnote':           { ka: 'EkkoShop არ გაუზიარებს შენს მონაცემებს მესამე მხარეებს.', en: 'EkkoShop never shares your data with third parties.' },
  'reg.visual.h2':          { ka: 'შემოუერთდი ათასობით ბედნიერ მომხმარებელს.', en: 'Join thousands of happy shoppers.' },
  'reg.visual.p':           { ka: 'მიიღე ექსკლუზიური შეთავაზებები, შეკვეთის თვალყურის დევნება და პერსონალური რეკომენდაციები.', en: 'Get access to exclusive deals, order tracking, and personalised recommendations.' },
  'reg.perk1':              { ka: '✓ უფასო მიწოდება ₾50-ზე მეტ შეკვეთებზე', en: '✓ Free shipping on orders over ₾50' },
  'reg.perk2':              { ka: '✓ 2 წლის გარანტია ყველა პროდუქტზე', en: '✓ 2-year warranty on all products' },
  'reg.perk3':              { ka: '✓ 30-დღიანი დაბრუნება',           en: '✓ 30-day hassle-free returns' },
  'reg.perk4':              { ka: '✓ ექსკლუზიური ფასდაკლებები',     en: '✓ Exclusive member-only discounts' },

  /* ── Verify email page ── */
  'verify.title':           { ka: 'ელ-ფოსტის დადასტურება',          en: 'Verify your email' },
  'verify.sub':             { ka: 'შეიყვანე ჩვენ მიერ გამოგზავნილი კოდი.', en: 'Enter the code we sent to your email address.' },
  'verify.email':           { ka: 'ელ-ფოსტა',                       en: 'Email Address' },
  'verify.code':            { ka: 'დადასტურების კოდი',               en: 'Verification Code' },
  'verify.placeholder':     { ka: '6-ნიშნა კოდი',                   en: 'Enter 6-digit code' },
  'verify.btn':             { ka: 'ელ-ფოსტის დადასტურება',          en: 'Verify Email' },
  'verify.loading':         { ka: 'მოწმდება…',                       en: 'Verifying…' },
  'verify.resend':          { ka: '↺ კოდის ხელახლა გაგზავნა',       en: '↺ Resend verification email' },
  'verify.alreadyVerified': { ka: 'უკვე დადასტურებული?',            en: 'Already verified?' },
  'verify.loginLink':       { ka: 'შედი →',                          en: 'Log in →' },
  'verify.noCode':          { ka: 'კოდი არ მიგიღია?',               en: "didn't get the code?" },
  'verify.success':         { ka: '✓ ელ-ფოსტა დადასტურდა! გადამისამართება…', en: '✓ Email verified! Redirecting to login…' },
  'verify.toasted':         { ka: 'ელ-ფოსტა წარმატებით დადასტურდა!', en: 'Email verified successfully!' },
  'verify.visual.h2':       { ka: 'ბოლო ნაბიჯი.',                   en: 'One last step.' },
  'verify.visual.p':        { ka: 'გამოგიგზავნეთ დადასტურების კოდი. შეიყვანე იგი ანგარიშის გასააქტიურებლად.', en: 'We sent a verification code to your email. Enter it to activate your account and start shopping.' },
  'verify.hint1':           { ka: '📧 შეამოწმე შენი inbox (და spam)', en: '📧 Check your inbox (and spam folder)' },
  'verify.hint2':           { ka: '⏱ კოდი 15 წუთში ვადაგასდება',   en: '⏱ Code expires in 15 minutes' },
  'verify.hint3':           { ka: '🔁 შეგიძლია ახალი კოდის მოთხოვნა', en: '🔁 You can request a new code below' },

  /* ── Forgot password page ── */
  'forgot.step1.title':     { ka: 'დაგავიწყდა პაროლი?',             en: 'Forgot password?' },
  'forgot.step1.sub':       { ka: 'არ ინერვიულო, გამოგიგზავნით აღდგენის კოდს.', en: "No worries, we'll send you a recovery code." },
  'forgot.step1.email':     { ka: 'ელ-ფოსტა',                       en: 'Email Address' },
  'forgot.step1.btn':       { ka: 'კოდის გაგზავნა',                 en: 'Send Recovery Code' },
  'forgot.step1.sending':   { ka: 'იგზავნება…',                      en: 'Sending…' },
  'forgot.step1.back':      { ka: 'გახსოვს? შესვლაზე დაბრუნება →',  en: 'Remembered it? Back to login →' },
  'forgot.step2.title':     { ka: 'შეიყვანე კოდი',                  en: 'Enter the code' },
  'forgot.step2.code':      { ka: 'აღდგენის კოდი',                  en: 'Recovery Code' },
  'forgot.step2.placeholder':{ ka: 'კოდი ელ-ფოსტიდან',             en: 'Enter code from email' },
  'forgot.step2.btn':       { ka: 'კოდის დადასტურება',              en: 'Verify Code' },
  'forgot.step2.verifying': { ka: 'მოწმდება…',                       en: 'Verifying…' },
  'forgot.step2.resend':    { ka: '↺ კოდის ხელახლა გაგზავნა',       en: '↺ Resend code' },
  'forgot.step2.change':    { ka: '← ელ-ფოსტის შეცვლა',             en: '← Change email' },
  'forgot.step3.title':     { ka: 'ახალი პაროლი',                   en: 'New password' },
  'forgot.step3.sub':       { ka: 'შეარჩიე ძლიერი პაროლი შენი ანგარიშისთვის.', en: 'Choose a strong password for your account.' },
  'forgot.step3.new':       { ka: 'ახალი პაროლი',                   en: 'New Password' },
  'forgot.step3.confirm':   { ka: 'პაროლის დადასტურება',             en: 'Confirm Password' },
  'forgot.step3.btn':       { ka: 'პაროლის განახლება',               en: 'Reset Password' },
  'forgot.step3.resetting': { ka: 'განახლება…',                       en: 'Resetting…' },
  'forgot.step3.success':   { ka: '✓ პაროლი განახლდა! გადამისამართება…', en: '✓ Password reset! Redirecting to login…' },
  'forgot.step3.toasted':   { ka: 'პაროლი წარმატებით განახლდა!',    en: 'Password reset successfully!' },
  'forgot.rstep1':          { ka: 'ელ-ფოსტის შეყვანა',              en: 'Enter Email' },
  'forgot.rstep1.desc':     { ka: 'გამოვუგზავნით კოდს',             en: "We'll send a recovery code" },
  'forgot.rstep2':          { ka: 'კოდის დადასტურება',              en: 'Verify Code' },
  'forgot.rstep2.desc':     { ka: 'შეამოწმე inbox',                  en: 'Check your inbox' },
  'forgot.rstep3':          { ka: 'ახალი პაროლი',                   en: 'New Password' },
  'forgot.rstep3.desc':     { ka: 'შეარჩიე ძლიერი პაროლი',          en: 'Choose a strong password' },
  'forgot.hint1':           { ka: 'შეიყვანე EkkoShop-ის ანგარიშთან დაკავშირებული ელ-ფოსტა.', en: 'Enter the email address associated with your EkkoShop account.' },
  'forgot.hint3':           { ka: 'თითქმის გათავდა — შეარჩიე ახალი ძლიერი პაროლი შენი ანგარიშისთვის.', en: 'Almost there — choose a new strong password for your account.' },

  /* ── Toasts / errors ── */
  'toast.loggedOut':        { ka: 'გამოსვლა.',                       en: 'Logged out.' },
  'toast.loadFailed':       { ka: 'პროდუქტების ჩატვირთვა ვერ მოხერხდა', en: 'Failed to load products' },
  'toast.productDeleted':   { ka: 'პროდუქტი წაიშალა',               en: 'Product deleted' },
  'toast.deleteFailed':     { ka: 'წაშლა ვერ მოხერხდა',             en: 'Delete failed' },
  'toast.codeSent':         { ka: 'კოდი გაიგზავნა!',                en: 'Recovery code sent!' },
  'toast.newCodeSent':      { ka: 'ახალი კოდი გაიგზავნა!',          en: 'New code sent!' },
  'toast.codeVerified':     { ka: 'კოდი დადასტურდა!',               en: 'Code verified!' },
  'toast.cantReach':        { ka: 'სერვერთან კავშირი ვერ მოხერხდა.', en: 'Cannot reach the server.' },
  'toast.resendFailed':     { ka: 'კოდის ხელახლა გაგზავნა ვერ მოხერხდა', en: 'Failed to resend code' },

  /* ── Wishlist ── */
  'wishlist.title':         { ka: 'სურვილების სია',                  en: 'Wishlist' },
  'wishlist.empty':         { ka: 'სურვილების სია ცარიელია',         en: 'Your wishlist is empty' },
  'wishlist.emptySub':      { ka: 'დააჭირე ♡ პროდუქტის სანახავად',  en: 'Click ♡ on any product to save it here' },
  'wishlist.added':         { ka: 'სურვილების სიაში დაემატა',        en: 'Added to wishlist' },
  'wishlist.removed':       { ka: 'სურვილების სიიდან ამოიღო',        en: 'Removed from wishlist' },
  'wishlist.addToCart':     { ka: '🛒 კალათაში',                     en: '🛒 Add to Cart' },
  'wishlist.remove':        { ka: '✕ ამოღება',                       en: '✕ Remove' },
  'wishlist.viewAll':       { ka: 'ყველას ნახვა →',                  en: 'View all →' },
  'wishlist.count':         { ka: 'ნივთი',                           en: 'items' },

  /* ── Errors ── */
  'err.emailRequired':      { ka: 'ელ-ფოსტა სავალდებულოა',          en: 'Email is required' },
  'err.passwordRequired':   { ka: 'პაროლი სავალდებულოა',             en: 'Password is required' },
  'err.invalidEmail':       { ka: 'სწორი ელ-ფოსტა სავალდებულოა',   en: 'Valid email required' },
  'err.passwordMin':        { ka: 'მინიმუმ 8 სიმბოლო',              en: 'Minimum 8 characters' },
  'err.passwordMatch':      { ka: 'პაროლები არ ემთხვევა',            en: 'Passwords do not match' },
  'err.firstRequired':      { ka: 'სავალდებულოა',                    en: 'Required' },
  'err.lastRequired':       { ka: 'სავალდებულოა',                    en: 'Required' },
  'err.acceptTerms':        { ka: 'უნდა დაეთანხმო სარგებლობის პირობებს.', en: 'You must accept the Terms of Service.' },
  'err.codeRequired':       { ka: 'კოდი სავალდებულოა',              en: 'Code is required' },
  'err.wrongPassword':      { ka: 'არასწორი ელ-ფოსტა ან პაროლი.',  en: 'Invalid email or password.' },
  'err.cantReach':          { ka: 'სერვერთან კავშირი ვერ მოხერხდა. API მუშაობს https://localhost:7060-ზე?', en: 'Cannot reach the server. Is the API running on https://localhost:7060?' },
};

/* ══════════════════════════════════════
   Language state — persisted in localStorage
══════════════════════════════════════ */
const LANG_KEY = 'ekko_lang';

export function getLang() {
  return localStorage.getItem(LANG_KEY) || 'ka';
}

export function setLang(lang) {
  localStorage.setItem(LANG_KEY, lang);
}

export function t(key) {
  const lang   = getLang();
  const entry  = TRANSLATIONS[key];
  if (!entry) { console.warn(`[i18n] missing key: "${key}"`); return key; }
  return entry[lang] ?? entry['en'] ?? key;
}

/* Translate a category name */
export function tCat(name) {
  return t(`cat.${name}`) !== `cat.${name}` ? t(`cat.${name}`) : name;
}
